# Passenger Travel Share - Test UI

A simple standalone HTML/JavaScript UI for testing the Passenger Travel Share API.

## Features

- Clean, modern interface
- Form validation
- Real-time API communication
- Success/Error feedback
- Configurable API endpoint
- No build process required

## Usage

### Option 1: Open directly in browser
Simply open `index.html` in your web browser.

**Note:** Modern browsers may block CORS requests when opening files directly. Use Option 2 or 3 for best results.

### Option 2: Using a simple HTTP server

#### Python 3:
```bash
cd ui
python -m http.server 8080
```
Then open: http://localhost:8080

#### Node.js (http-server):
```bash
npx http-server ui -p 8080
```
Then open: http://localhost:8080

#### VS Code Live Server:
Install the "Live Server" extension and right-click on `index.html` → "Open with Live Server"

### Option 3: Using any web server
Place the `ui` folder in any web server directory and access it via HTTP.

## Configuration

The API endpoint can be configured directly in the UI. Default is `http://localhost:5008`.

## API Endpoint

The UI calls: `POST /api/passenger/travel-share`

## Requirements

- Modern web browser (Chrome, Firefox, Edge, Safari)
- API server running (default: http://localhost:5008)

## Notes

- This is a standalone frontend that can be moved to a separate project
- No dependencies or build process required
- Pure HTML, CSS, and vanilla JavaScript
- Can be easily integrated into React, Vue, or Angular projects later


