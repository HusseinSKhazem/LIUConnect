// Car makes and models mapping
const carModels = {
    'toyota': ['Camry', 'Corolla', 'RAV4', 'Highlander', 'Prius'],
    'honda': ['Civic', 'Accord', 'CR-V', 'Pilot', 'Odyssey'],
    'ford': ['F-150', 'Mustang', 'Escape', 'Explorer', 'Fusion'],
    'chevrolet': ['Silverado', 'Malibu', 'Equinox', 'Tahoe', 'Camaro'],
    'bmw': ['3 Series', '5 Series', 'X3', 'X5', 'M4'],
    'mercedes-benz': ['C-Class', 'E-Class', 'GLC', 'GLE', 'S-Class'],
    'audi': ['A4', 'A6', 'Q5', 'Q7', 'A3'],
    'nissan': ['Altima', 'Rogue', 'Sentra', 'Maxima', 'Pathfinder'],
    'hyundai': ['Elantra', 'Sonata', 'Tucson', 'Santa Fe', 'Kona'],
    'tesla': ['Model 3', 'Model S', 'Model X', 'Model Y']
};

// DOM Elements
const carForm = document.getElementById('carForm');
const makeSelect = document.getElementById('make');
const modelSelect = document.getElementById('model');
const loading = document.getElementById('loading');
const resultsSection = document.getElementById('resultsSection');
const errorMessage = document.getElementById('errorMessage');
const errorText = document.getElementById('errorText');
const closeError = document.getElementById('closeError');
const tryAgainBtn = document.getElementById('tryAgainBtn');

// Event Listeners
makeSelect.addEventListener('change', updateModelOptions);
carForm.addEventListener('submit', handleFormSubmit);
closeError.addEventListener('click', hideError);
tryAgainBtn.addEventListener('click', resetApp);

// Update model options based on selected make
function updateModelOptions() {
    const selectedMake = makeSelect.value;
    modelSelect.innerHTML = '<option value="">Select Model</option>';

    if (selectedMake && carModels[selectedMake]) {
        carModels[selectedMake].forEach(model => {
            const option = document.createElement('option');
            option.value = model.toLowerCase().replace(/\s+/g, ' ');
            option.textContent = model;
            modelSelect.appendChild(option);
        });
        modelSelect.disabled = false;
    } else {
        modelSelect.disabled = true;
    }
}

// Handle form submission
async function handleFormSubmit(e) {
    e.preventDefault();

    // Hide previous results and errors
    resultsSection.classList.add('hidden');
    hideError();

    // Show loading
    loading.classList.remove('hidden');
    carForm.classList.add('hidden');

    // Collect form data
    const formData = {
        make: document.getElementById('make').value,
        model: document.getElementById('model').value,
        year: parseInt(document.getElementById('year').value),
        mileage: parseInt(document.getElementById('mileage').value),
        condition: document.getElementById('condition').value,
        transmission: document.getElementById('transmission').value,
        fuel_type: document.getElementById('fuel_type').value,
        body_type: document.getElementById('body_type').value,
        engine_size: parseFloat(document.getElementById('engine_size').value)
    };

    try {
        const response = await fetch('/estimate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        const data = await response.json();

        loading.classList.add('hidden');

        if (response.ok) {
            displayResults(data, formData);
        } else {
            carForm.classList.remove('hidden');
            showError(data.error || 'Failed to estimate price. Please try again.');
        }
    } catch (error) {
        loading.classList.add('hidden');
        carForm.classList.remove('hidden');
        showError('Network error. Please check your connection and try again.');
        console.error('Error:', error);
    }
}

// Display results
function displayResults(data, formData) {
    // Update price
    document.getElementById('priceValue').textContent = formatNumber(Math.round(data.price));
    document.getElementById('minPrice').textContent = formatNumber(Math.round(data.min_price));
    document.getElementById('maxPrice').textContent = formatNumber(Math.round(data.max_price));

    // Update confidence
    const confidencePercent = data.confidence * 100;
    document.getElementById('confidence').textContent = confidencePercent.toFixed(1);
    document.getElementById('confidenceFill').style.width = confidencePercent + '%';

    // Update details
    const detailsGrid = document.getElementById('detailsGrid');
    detailsGrid.innerHTML = '';

    // Create detail items for all vehicle information
    const details = {
        'Make': capitalizeWords(formData.make),
        'Model': capitalizeWords(formData.model),
        'Year': formData.year,
        'Mileage': formatNumber(formData.mileage) + ' miles',
        'Condition': capitalizeWords(formData.condition),
        'Transmission': capitalizeWords(formData.transmission),
        'Fuel Type': capitalizeWords(formData.fuel_type),
        'Body Type': capitalizeWords(formData.body_type),
        'Engine Size': formData.engine_size + 'L'
    };

    for (const [label, value] of Object.entries(details)) {
        const detailItem = document.createElement('div');
        detailItem.className = 'detail-item';
        detailItem.innerHTML = `
            <div class="detail-label">${label}</div>
            <div class="detail-value">${value}</div>
        `;
        detailsGrid.appendChild(detailItem);
    }

    // Show results
    resultsSection.classList.remove('hidden');
    resultsSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
}

// Format number with commas
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
}

// Capitalize words
function capitalizeWords(str) {
    return str.split(/[\s-]/)
        .map(word => word.charAt(0).toUpperCase() + word.slice(1))
        .join(' ');
}

// Show error message
function showError(message) {
    errorText.textContent = message;
    errorMessage.classList.remove('hidden');
    errorMessage.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

// Hide error message
function hideError() {
    errorMessage.classList.add('hidden');
}

// Reset app
function resetApp() {
    carForm.reset();
    modelSelect.disabled = true;
    resultsSection.classList.add('hidden');
    carForm.classList.remove('hidden');
    hideError();
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

// Initialize
document.addEventListener('DOMContentLoaded', () => {
    // Set current year as max for year input
    const currentYear = new Date().getFullYear();
    document.getElementById('year').max = currentYear;

    // Disable model select initially
    modelSelect.disabled = true;
});
