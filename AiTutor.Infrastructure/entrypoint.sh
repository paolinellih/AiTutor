#!/bin/sh
# Start Ollama server in the background
ollama serve &

# Wait a bit for the server to start
sleep 2

# Pull a lightweight model
ollama pull tinyllama  # Replace with another model if needed

# Wait for Ollama to stay active
wait $!