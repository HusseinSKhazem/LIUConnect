# Car Price Estimator

A simple web application that estimates car prices based on uploaded car images using Flask backend and HTML/CSS/JS frontend.

## Features

- 🚗 Upload car images via drag-and-drop or file browser
- 💰 Get estimated car price with confidence range
- 📊 View detected features and image analysis
- 🎨 Modern, responsive UI design
- ⚡ Real-time image preview
- 🔄 Easy to use interface

## Technology Stack

- **Backend**: Flask (Python)
- **Frontend**: HTML5, CSS3, JavaScript
- **Image Processing**: Pillow (PIL)

## Project Structure

```
car-price-estimator/
├── app.py                  # Flask application
├── requirements.txt        # Python dependencies
├── README.md              # This file
├── static/
│   ├── css/
│   │   └── style.css      # Styling
│   ├── js/
│   │   └── script.js      # Frontend logic
│   └── uploads/           # Uploaded images storage
└── templates/
    └── index.html         # Main HTML page
```

## Installation

### Prerequisites

- Python 3.7 or higher
- pip (Python package manager)

### Setup Steps

1. Clone or download this repository

2. Navigate to the project directory:
```bash
cd car-price-estimator
```

3. Create a virtual environment (recommended):
```bash
python -m venv venv
```

4. Activate the virtual environment:
   - **Windows**:
     ```bash
     venv\Scripts\activate
     ```
   - **Mac/Linux**:
     ```bash
     source venv/bin/activate
     ```

5. Install dependencies:
```bash
pip install -r requirements.txt
```

## Usage

1. Start the Flask server:
```bash
python app.py
```

2. Open your web browser and navigate to:
```
http://localhost:5000
```

3. Upload a car image:
   - Drag and drop an image onto the upload area, OR
   - Click "Browse Files" to select an image

4. Click "Estimate Price" to get the estimation

5. View the results including:
   - Estimated price
   - Price range (min-max)
   - Confidence level
   - Detected features

## Supported Image Formats

- PNG
- JPG/JPEG
- GIF
- WEBP

Maximum file size: 16MB

## API Endpoints

### `GET /`
Returns the main HTML page

### `POST /estimate`
Estimates car price from uploaded image

**Request:**
- Content-Type: `multipart/form-data`
- Body: `car_image` (file)

**Response:**
```json
{
  "price": 25000.00,
  "min_price": 21250.00,
  "max_price": 28750.00,
  "confidence": 0.85,
  "features_detected": {
    "image_quality": "Good",
    "resolution": "1920x1080"
  },
  "image_url": "/static/uploads/car.jpg"
}
```

### `GET /health`
Health check endpoint

**Response:**
```json
{
  "status": "healthy"
}
```

## Important Notes

⚠️ **This is a demonstration application**

The current implementation uses a **mock estimation algorithm** based on basic image properties (brightness, resolution, etc.). For production use, you should:

1. **Integrate a Machine Learning Model**: Replace the `estimate_car_price()` function with a trained CNN model that can:
   - Detect car make and model
   - Identify car condition
   - Recognize features (leather seats, sunroof, etc.)
   - Estimate accurate market prices

2. **Use Real Data**: Train your model on actual car images with corresponding prices

3. **Add More Features**:
   - Car make/model detection
   - Year estimation
   - Condition assessment
   - Mileage input
   - Location-based pricing

## Customization

### Changing the Port

Edit `app.py` line 104:
```python
app.run(debug=True, host='0.0.0.0', port=5000)  # Change port here
```

### Adjusting Upload Limits

Edit `app.py` line 10:
```python
app.config['MAX_CONTENT_LENGTH'] = 16 * 1024 * 1024  # Change size limit
```

### Modifying Estimation Logic

Edit the `estimate_car_price()` function in `app.py` to implement your custom logic or ML model.

## Troubleshooting

### Port Already in Use
If port 5000 is already in use, change the port in `app.py` or kill the process using the port:
```bash
# Find process on port 5000
lsof -i :5000
# Kill the process
kill -9 <PID>
```

### Module Not Found Error
Make sure you've activated the virtual environment and installed all dependencies:
```bash
pip install -r requirements.txt
```

### Upload Folder Permissions
Ensure the application has write permissions to create the `static/uploads` folder.

## Future Enhancements

- [ ] Integrate real ML model (e.g., TensorFlow, PyTorch)
- [ ] Add car make/model detection
- [ ] Database for storing estimations
- [ ] User authentication
- [ ] Historical price tracking
- [ ] Comparison with market prices
- [ ] Export results as PDF
- [ ] Mobile app version

## License

This project is provided as-is for educational and demonstration purposes.

## Disclaimer

This tool provides estimated prices for demonstration purposes only. For accurate car valuations, please consult with professional automotive appraisers and use established valuation services.
