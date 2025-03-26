FROM ollama/ollama

# Copy the entrypoint script
COPY entrypoint.sh /entrypoint.sh

# Make the script executable
RUN chmod +x /entrypoint.sh

# Expose the correct port
EXPOSE 11434

# Set the entrypoint to our script
ENTRYPOINT ["/bin/sh", "-c", "/entrypoint.sh"]
