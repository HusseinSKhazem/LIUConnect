"""
Car Price Prediction Model
Production-ready ML model using Random Forest Regressor
"""
import pickle
import numpy as np
from sklearn.ensemble import RandomForestRegressor
from sklearn.preprocessing import LabelEncoder
import os

class CarPricePredictor:
    def __init__(self):
        self.model = None
        self.label_encoders = {}
        self.feature_names = [
            'make', 'model', 'year', 'mileage', 'condition',
            'transmission', 'fuel_type', 'body_type', 'engine_size'
        ]

    def train(self, training_data):
        """Train the model on provided data"""
        # Prepare features and target
        X = []
        y = []

        # Initialize label encoders for categorical features
        categorical_features = ['make', 'model', 'condition', 'transmission', 'fuel_type', 'body_type']

        for feature in categorical_features:
            self.label_encoders[feature] = LabelEncoder()

        # Collect all unique values first for fitting encoders
        for feature in categorical_features:
            values = [str(item[feature]).strip().lower() for item in training_data]
            self.label_encoders[feature].fit(values)

        # Encode features
        for item in training_data:
            features = []

            # Encode categorical features
            for feature in categorical_features:
                value = str(item[feature]).strip().lower()
                encoded = self.label_encoders[feature].transform([value])[0]
                features.append(encoded)

            # Add numerical features
            features.append(float(item['year']))
            features.append(float(item['mileage']))
            features.append(float(item['engine_size']))

            X.append(features)
            y.append(float(item['price']))

        # Train Random Forest model
        self.model = RandomForestRegressor(
            n_estimators=100,
            max_depth=10,
            min_samples_split=5,
            min_samples_leaf=2,
            random_state=42
        )

        self.model.fit(np.array(X), np.array(y))

    def predict(self, car_features):
        """Predict price for a car with given features"""
        if self.model is None:
            raise ValueError("Model not trained yet")

        # Encode categorical features
        categorical_features = ['make', 'model', 'condition', 'transmission', 'fuel_type', 'body_type']
        features = []

        for feature in categorical_features:
            value = str(car_features[feature]).strip().lower()

            # Handle unknown values
            if value not in self.label_encoders[feature].classes_:
                # Use the most common class as default
                value = self.label_encoders[feature].classes_[0]

            encoded = self.label_encoders[feature].transform([value])[0]
            features.append(encoded)

        # Add numerical features
        features.append(float(car_features['year']))
        features.append(float(car_features['mileage']))
        features.append(float(car_features['engine_size']))

        # Make prediction
        prediction = self.model.predict([features])[0]

        # Calculate confidence based on model's estimators
        predictions = []
        for estimator in self.model.estimators_:
            pred = estimator.predict([features])[0]
            predictions.append(pred)

        std_dev = np.std(predictions)
        confidence = max(0.6, min(0.95, 1 - (std_dev / prediction)))

        return {
            'price': max(1000, prediction),  # Minimum $1,000
            'confidence': confidence,
            'std_dev': std_dev
        }

    def save(self, filepath='models/car_price_model.pkl'):
        """Save trained model to disk"""
        os.makedirs(os.path.dirname(filepath), exist_ok=True)
        with open(filepath, 'wb') as f:
            pickle.dump({
                'model': self.model,
                'label_encoders': self.label_encoders
            }, f)

    def load(self, filepath='models/car_price_model.pkl'):
        """Load trained model from disk"""
        if not os.path.exists(filepath):
            return False

        with open(filepath, 'rb') as f:
            data = pickle.load(f)
            self.model = data['model']
            self.label_encoders = data['label_encoders']

        return True
