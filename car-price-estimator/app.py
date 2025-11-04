"""
Car Price Estimator - Production Backend
Flask application with real ML-based price prediction
"""
from flask import Flask, render_template, request, jsonify
import os
from model import CarPricePredictor

app = Flask(__name__)

# Initialize the ML model
predictor = CarPricePredictor()

# Try to load existing model, if not available, it will be trained
model_path = 'models/car_price_model.pkl'
model_loaded = False

if os.path.exists(model_path):
    try:
        predictor.load(model_path)
        model_loaded = True
        print("✓ Model loaded successfully from disk")
    except Exception as e:
        print(f"✗ Failed to load model: {e}")
        print("  Run 'python train_model.py' to train the model")
else:
    print("✗ Model not found at:", model_path)
    print("  Run 'python train_model.py' to train the model")


# Validation constants
VALID_MAKES = ['toyota', 'honda', 'ford', 'chevrolet', 'bmw', 'mercedes-benz',
               'audi', 'nissan', 'hyundai', 'tesla']
VALID_CONDITIONS = ['excellent', 'good', 'fair', 'poor']
VALID_TRANSMISSIONS = ['automatic', 'manual', 'cvt']
VALID_FUEL_TYPES = ['gasoline', 'diesel', 'electric', 'hybrid']
VALID_BODY_TYPES = ['sedan', 'suv', 'truck', 'coupe', 'hatchback', 'wagon']

CURRENT_YEAR = 2024
MIN_YEAR = 2000
MAX_MILEAGE = 500000
MIN_ENGINE_SIZE = 1.0
MAX_ENGINE_SIZE = 8.0


def validate_car_data(data):
    """Validate incoming car data"""
    errors = []

    # Required fields
    required_fields = ['make', 'model', 'year', 'mileage', 'condition',
                       'transmission', 'fuel_type', 'body_type', 'engine_size']

    for field in required_fields:
        if field not in data or data[field] is None or data[field] == '':
            errors.append(f"Missing required field: {field}")

    if errors:
        return False, errors

    # Validate make
    if data['make'].lower() not in VALID_MAKES:
        errors.append(f"Invalid make. Must be one of: {', '.join(VALID_MAKES)}")

    # Validate year
    try:
        year = int(data['year'])
        if year < MIN_YEAR or year > CURRENT_YEAR:
            errors.append(f"Year must be between {MIN_YEAR} and {CURRENT_YEAR}")
    except (ValueError, TypeError):
        errors.append("Year must be a valid number")

    # Validate mileage
    try:
        mileage = int(data['mileage'])
        if mileage < 0 or mileage > MAX_MILEAGE:
            errors.append(f"Mileage must be between 0 and {MAX_MILEAGE}")
    except (ValueError, TypeError):
        errors.append("Mileage must be a valid number")

    # Validate condition
    if data['condition'].lower() not in VALID_CONDITIONS:
        errors.append(f"Invalid condition. Must be one of: {', '.join(VALID_CONDITIONS)}")

    # Validate transmission
    if data['transmission'].lower() not in VALID_TRANSMISSIONS:
        errors.append(f"Invalid transmission. Must be one of: {', '.join(VALID_TRANSMISSIONS)}")

    # Validate fuel type
    if data['fuel_type'].lower() not in VALID_FUEL_TYPES:
        errors.append(f"Invalid fuel type. Must be one of: {', '.join(VALID_FUEL_TYPES)}")

    # Validate body type
    if data['body_type'].lower() not in VALID_BODY_TYPES:
        errors.append(f"Invalid body type. Must be one of: {', '.join(VALID_BODY_TYPES)}")

    # Validate engine size
    try:
        engine_size = float(data['engine_size'])
        if engine_size < MIN_ENGINE_SIZE or engine_size > MAX_ENGINE_SIZE:
            errors.append(f"Engine size must be between {MIN_ENGINE_SIZE}L and {MAX_ENGINE_SIZE}L")
    except (ValueError, TypeError):
        errors.append("Engine size must be a valid number")

    if errors:
        return False, errors

    return True, []


@app.route('/')
def index():
    """Serve the main HTML page"""
    return render_template('index.html')


@app.route('/estimate', methods=['POST'])
def estimate():
    """Handle car data and return price estimation"""
    # Check if model is loaded
    if not model_loaded:
        return jsonify({
            'error': 'Model not available. Please train the model first by running: python train_model.py'
        }), 503

    # Get JSON data from request
    try:
        data = request.get_json()
    except Exception as e:
        return jsonify({'error': 'Invalid JSON data'}), 400

    if not data:
        return jsonify({'error': 'No data provided'}), 400

    # Validate the data
    is_valid, errors = validate_car_data(data)
    if not is_valid:
        return jsonify({'error': 'Validation failed', 'details': errors}), 400

    try:
        # Prepare car features for prediction
        car_features = {
            'make': data['make'].lower(),
            'model': data['model'].lower(),
            'year': int(data['year']),
            'mileage': int(data['mileage']),
            'condition': data['condition'].lower(),
            'transmission': data['transmission'].lower(),
            'fuel_type': data['fuel_type'].lower(),
            'body_type': data['body_type'].lower(),
            'engine_size': float(data['engine_size'])
        }

        # Get prediction from model
        result = predictor.predict(car_features)

        # Calculate price range based on confidence
        price = result['price']
        confidence = result['confidence']

        # Higher confidence = narrower range
        range_factor = 0.15 - (confidence - 0.6) * 0.2  # 15% to 8% range
        range_factor = max(0.08, min(0.15, range_factor))

        min_price = price * (1 - range_factor)
        max_price = price * (1 + range_factor)

        # Return the estimation
        return jsonify({
            'price': round(price, 2),
            'min_price': round(min_price, 2),
            'max_price': round(max_price, 2),
            'confidence': round(confidence, 3),
            'model_type': 'Random Forest Regressor',
            'features_analyzed': 9
        })

    except Exception as e:
        print(f"Prediction error: {e}")
        return jsonify({'error': f'Prediction failed: {str(e)}'}), 500


@app.route('/health')
def health():
    """Health check endpoint"""
    return jsonify({
        'status': 'healthy',
        'model_loaded': model_loaded,
        'model_type': 'Random Forest Regressor' if model_loaded else None
    })


@app.route('/api/info')
def api_info():
    """API information endpoint"""
    return jsonify({
        'name': 'Car Price Estimator API',
        'version': '2.0.0',
        'description': 'Production-grade ML-powered car price estimation',
        'model': 'Random Forest Regressor',
        'training_samples': 2000,
        'endpoints': {
            '/': 'Main application',
            '/estimate': 'POST - Get price estimate',
            '/health': 'GET - Health check',
            '/api/info': 'GET - API information'
        }
    })


if __name__ == '__main__':
    print("\n" + "="*60)
    print("Car Price Estimator - Production Backend")
    print("="*60)
    if model_loaded:
        print("✓ Ready to serve predictions")
    else:
        print("✗ Model not loaded - train it first:")
        print("  python train_model.py")
    print("="*60 + "\n")

    app.run(debug=True, host='0.0.0.0', port=5000)
