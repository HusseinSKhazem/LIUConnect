from flask import Flask, render_template, request, jsonify
from werkzeug.utils import secure_filename
import os
from PIL import Image
import random

app = Flask(__name__)
app.config['UPLOAD_FOLDER'] = 'static/uploads'
app.config['MAX_CONTENT_LENGTH'] = 16 * 1024 * 1024  # 16MB max file size
ALLOWED_EXTENSIONS = {'png', 'jpg', 'jpeg', 'gif', 'webp'}

# Ensure upload folder exists
os.makedirs(app.config['UPLOAD_FOLDER'], exist_ok=True)


def allowed_file(filename):
    """Check if the uploaded file has an allowed extension"""
    return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


def estimate_car_price(image_path):
    """
    Simple car price estimation based on image analysis.
    In a real application, this would use a trained ML model.
    For now, we'll use basic image properties to generate a mock estimate.
    """
    try:
        img = Image.open(image_path)
        width, height = img.size

        # Mock estimation based on image properties
        # In reality, you'd use a CNN model trained on car images and prices

        # Get average color brightness (simple feature)
        img_rgb = img.convert('RGB')
        pixels = list(img_rgb.getdata())
        avg_brightness = sum([sum(pixel) / 3 for pixel in pixels[:1000]]) / min(1000, len(pixels))

        # Base price calculation (mock)
        base_price = 15000

        # Add variation based on image properties (mock logic)
        brightness_factor = (avg_brightness / 255) * 10000
        resolution_factor = (width * height / 1000000) * 5000
        random_factor = random.uniform(-5000, 15000)

        estimated_price = base_price + brightness_factor + resolution_factor + random_factor
        estimated_price = max(5000, min(100000, estimated_price))  # Keep in reasonable range

        # Add some mock details
        confidence = random.uniform(0.65, 0.90)

        return {
            'price': round(estimated_price, 2),
            'min_price': round(estimated_price * 0.85, 2),
            'max_price': round(estimated_price * 1.15, 2),
            'confidence': round(confidence, 2),
            'features_detected': {
                'image_quality': 'Good' if width > 800 else 'Medium',
                'resolution': f'{width}x{height}'
            }
        }
    except Exception as e:
        return None


@app.route('/')
def index():
    """Serve the main HTML page"""
    return render_template('index.html')


@app.route('/estimate', methods=['POST'])
def estimate():
    """Handle image upload and return price estimation"""
    if 'car_image' not in request.files:
        return jsonify({'error': 'No file provided'}), 400

    file = request.files['car_image']

    if file.filename == '':
        return jsonify({'error': 'No file selected'}), 400

    if not allowed_file(file.filename):
        return jsonify({'error': 'Invalid file type. Please upload an image file.'}), 400

    try:
        # Save the file
        filename = secure_filename(file.filename)
        filepath = os.path.join(app.config['UPLOAD_FOLDER'], filename)
        file.save(filepath)

        # Estimate the price
        estimation = estimate_car_price(filepath)

        if estimation is None:
            return jsonify({'error': 'Failed to process the image'}), 500

        # Add the image URL to the response
        estimation['image_url'] = f'/static/uploads/{filename}'

        return jsonify(estimation)

    except Exception as e:
        return jsonify({'error': f'Server error: {str(e)}'}), 500


@app.route('/health')
def health():
    """Health check endpoint"""
    return jsonify({'status': 'healthy'})


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)
