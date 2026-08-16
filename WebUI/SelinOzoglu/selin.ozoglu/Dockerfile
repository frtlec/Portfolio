# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:latest AS build

# Set the working directory
WORKDIR /usr/local/app

# Add the source code to app
COPY WebUI/SelinOzoglu/selin.ozoglu /usr/local/app/

# Install all the dependencies
# --legacy-peer-deps: @ngrx/store's latest release (21.x) hasn't caught up to
# Angular 22's peer dependency range yet; it works fine at runtime.
RUN npm install --legacy-peer-deps

# Generate the build of the application
RUN npm run build --prod 


# Stage 2: Serve app with nginx server

# Use official nginx image as the base image
FROM nginx:latest
COPY WebUI/SelinOzoglu/selin.ozoglu/nginx.conf /etc/nginx/nginx.conf
# Copy the build output to replace the default nginx contents.
COPY --from=build /usr/local/app/dist/selin.ozoglu/browser /usr/share/nginx/html

# Expose port 80
EXPOSE 80