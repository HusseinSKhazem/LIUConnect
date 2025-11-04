"""
Training script for car price prediction model
Generates realistic training data and trains the model
"""
from model import CarPricePredictor
import random

def generate_training_data():
    """Generate realistic car training data"""

    makes_models = {
        'toyota': ['camry', 'corolla', 'rav4', 'highlander', 'prius'],
        'honda': ['civic', 'accord', 'cr-v', 'pilot', 'odyssey'],
        'ford': ['f-150', 'mustang', 'escape', 'explorer', 'fusion'],
        'chevrolet': ['silverado', 'malibu', 'equinox', 'tahoe', 'camaro'],
        'bmw': ['3 series', '5 series', 'x3', 'x5', 'm4'],
        'mercedes-benz': ['c-class', 'e-class', 'glc', 'gle', 's-class'],
        'audi': ['a4', 'a6', 'q5', 'q7', 'a3'],
        'nissan': ['altima', 'rogue', 'sentra', 'maxima', 'pathfinder'],
        'hyundai': ['elantra', 'sonata', 'tucson', 'santa fe', 'kona'],
        'tesla': ['model 3', 'model s', 'model x', 'model y']
    }

    conditions = ['excellent', 'good', 'fair', 'poor']
    transmissions = ['automatic', 'manual', 'cvt']
    fuel_types = ['gasoline', 'diesel', 'electric', 'hybrid']
    body_types = ['sedan', 'suv', 'truck', 'coupe', 'hatchback', 'wagon']

    # Base prices by make (premium vs economy)
    base_prices = {
        'toyota': 25000, 'honda': 24000, 'ford': 28000,
        'chevrolet': 27000, 'bmw': 45000, 'mercedes-benz': 50000,
        'audi': 42000, 'nissan': 23000, 'hyundai': 22000,
        'tesla': 55000
    }

    training_data = []
    current_year = 2024

    # Generate 2000 realistic car entries
    for _ in range(2000):
        make = random.choice(list(makes_models.keys()))
        model = random.choice(makes_models[make])
        year = random.randint(2010, 2024)
        mileage = random.randint(5000, 200000)
        condition = random.choice(conditions)
        transmission = random.choice(transmissions)
        fuel_type = random.choice(fuel_types)
        body_type = random.choice(body_types)
        engine_size = round(random.uniform(1.5, 5.0), 1)

        # Calculate price based on features
        base_price = base_prices[make]

        # Age depreciation (15% per year for first 5 years, then 8% per year)
        age = current_year - year
        if age <= 5:
            depreciation = 1 - (age * 0.15)
        else:
            depreciation = 1 - (5 * 0.15) - ((age - 5) * 0.08)
        depreciation = max(0.2, depreciation)  # Minimum 20% of original value

        price = base_price * depreciation

        # Mileage impact (reduce price by $0.10 per mile over 50k)
        if mileage > 50000:
            price -= (mileage - 50000) * 0.10

        # Condition impact
        condition_multipliers = {
            'excellent': 1.15,
            'good': 1.0,
            'fair': 0.85,
            'poor': 0.65
        }
        price *= condition_multipliers[condition]

        # Fuel type impact
        if fuel_type == 'electric':
            price *= 1.20
        elif fuel_type == 'hybrid':
            price *= 1.10

        # Body type impact
        if body_type in ['truck', 'suv']:
            price *= 1.15

        # Engine size impact
        price += (engine_size - 2.5) * 2000

        # Add some randomness (+/- 10%)
        price *= random.uniform(0.90, 1.10)

        # Ensure reasonable bounds
        price = max(3000, min(150000, price))

        training_data.append({
            'make': make,
            'model': model,
            'year': year,
            'mileage': mileage,
            'condition': condition,
            'transmission': transmission,
            'fuel_type': fuel_type,
            'body_type': body_type,
            'engine_size': engine_size,
            'price': round(price, 2)
        })

    return training_data

def main():
    """Train and save the model"""
    print("Generating training data...")
    training_data = generate_training_data()
    print(f"Generated {len(training_data)} training samples")

    print("\nTraining model...")
    predictor = CarPricePredictor()
    predictor.train(training_data)
    print("Model trained successfully!")

    print("\nSaving model...")
    predictor.save('models/car_price_model.pkl')
    print("Model saved to models/car_price_model.pkl")

    # Test prediction
    print("\nTesting prediction...")
    test_car = {
        'make': 'toyota',
        'model': 'camry',
        'year': 2020,
        'mileage': 35000,
        'condition': 'good',
        'transmission': 'automatic',
        'fuel_type': 'gasoline',
        'body_type': 'sedan',
        'engine_size': 2.5
    }

    result = predictor.predict(test_car)
    print(f"\nTest prediction for 2020 Toyota Camry:")
    print(f"  Estimated Price: ${result['price']:,.2f}")
    print(f"  Confidence: {result['confidence']*100:.1f}%")

if __name__ == '__main__':
    main()
