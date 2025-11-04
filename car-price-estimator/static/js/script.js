// Global variables
let selectedFile = null;

// DOM elements
const uploadArea = document.getElementById('uploadArea');
const fileInput = document.getElementById('fileInput');
const previewSection = document.getElementById('previewSection');
const previewImage = document.getElementById('previewImage');
const removeBtn = document.getElementById('removeBtn');
const estimateBtn = document.getElementById('estimateBtn');
const loading = document.getElementById('loading');
const resultsSection = document.getElementById('resultsSection');
const errorMessage = document.getElementById('errorMessage');
const errorText = document.getElementById('errorText');
const closeError = document.getElementById('closeError');
const tryAgainBtn = document.getElementById('tryAgainBtn');

// Event listeners
uploadArea.addEventListener('click', () => fileInput.click());
fileInput.addEventListener('change', handleFileSelect);
removeBtn.addEventListener('click', removeImage);
estimateBtn.addEventListener('click', estimatePrice);
closeError.addEventListener('click', hideError);
tryAgainBtn.addEventListener('click', resetApp);

// Drag and drop functionality
uploadArea.addEventListener('dragover', (e) => {
    e.preventDefault();
    uploadArea.classList.add('drag-over');
});

uploadArea.addEventListener('dragleave', () => {
    uploadArea.classList.remove('drag-over');
});

uploadArea.addEventListener('drop', (e) => {
    e.preventDefault();
    uploadArea.classList.remove('drag-over');

    const files = e.dataTransfer.files;
    if (files.length > 0) {
        handleFile(files[0]);
    }
});

// Handle file selection
function handleFileSelect(e) {
    const file = e.target.files[0];
    if (file) {
        handleFile(file);
    }
}

// Handle file
function handleFile(file) {
    // Check if file is an image
    if (!file.type.startsWith('image/')) {
        showError('Please select an image file.');
        return;
    }

    // Check file size (16MB max)
    if (file.size > 16 * 1024 * 1024) {
        showError('File size must be less than 16MB.');
        return;
    }

    selectedFile = file;

    // Display preview
    const reader = new FileReader();
    reader.onload = (e) => {
        previewImage.src = e.target.result;
        uploadArea.style.display = 'none';
        previewSection.style.display = 'block';
        estimateBtn.disabled = false;
    };
    reader.readAsDataURL(file);
}

// Remove image
function removeImage() {
    selectedFile = null;
    fileInput.value = '';
    uploadArea.style.display = 'block';
    previewSection.style.display = 'none';
    estimateBtn.disabled = true;
}

// Estimate price
async function estimatePrice() {
    if (!selectedFile) {
        showError('Please select an image first.');
        return;
    }

    // Hide previous results and errors
    resultsSection.style.display = 'none';
    hideError();

    // Show loading
    loading.style.display = 'block';

    // Create form data
    const formData = new FormData();
    formData.append('car_image', selectedFile);

    try {
        const response = await fetch('/estimate', {
            method: 'POST',
            body: formData
        });

        const data = await response.json();

        loading.style.display = 'none';

        if (response.ok) {
            displayResults(data);
        } else {
            showError(data.error || 'Failed to estimate price. Please try again.');
        }
    } catch (error) {
        loading.style.display = 'none';
        showError('Network error. Please check your connection and try again.');
        console.error('Error:', error);
    }
}

// Display results
function displayResults(data) {
    // Update price
    document.getElementById('priceValue').textContent = formatNumber(data.price);
    document.getElementById('minPrice').textContent = formatNumber(data.min_price);
    document.getElementById('maxPrice').textContent = formatNumber(data.max_price);

    // Update confidence
    const confidencePercent = data.confidence * 100;
    document.getElementById('confidence').textContent = confidencePercent.toFixed(0);
    document.getElementById('confidenceFill').style.width = confidencePercent + '%';

    // Update details
    const detailsGrid = document.getElementById('detailsGrid');
    detailsGrid.innerHTML = '';

    if (data.features_detected) {
        for (const [key, value] of Object.entries(data.features_detected)) {
            const detailItem = document.createElement('div');
            detailItem.className = 'detail-item';
            detailItem.innerHTML = `
                <div class="detail-label">${formatLabel(key)}</div>
                <div class="detail-value">${value}</div>
            `;
            detailsGrid.appendChild(detailItem);
        }
    }

    // Show results
    resultsSection.style.display = 'block';
    resultsSection.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

// Format number with commas
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
}

// Format label (convert snake_case to Title Case)
function formatLabel(str) {
    return str
        .split('_')
        .map(word => word.charAt(0).toUpperCase() + word.slice(1))
        .join(' ');
}

// Show error message
function showError(message) {
    errorText.textContent = message;
    errorMessage.style.display = 'block';
    errorMessage.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

// Hide error message
function hideError() {
    errorMessage.style.display = 'none';
}

// Reset app
function resetApp() {
    removeImage();
    resultsSection.style.display = 'none';
    hideError();
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

// Format file size
function formatFileSize(bytes) {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i];
}
