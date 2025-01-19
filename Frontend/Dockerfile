# Step 1: Use an official Node.js image as the base image
FROM node:18-alpine AS build

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json (or yarn.lock) to install dependencies
COPY package*.json ./

# Install the dependencies
RUN npm install

# Copy the entire app source code into the container
COPY . .

# Build the React app for production
RUN npm run build

# Step 2: Set up the production environment
FROM nginx:alpine

# Copy the built React app into the Nginx container
COPY --from=build /app/dist /usr/share/nginx/html

#Copy the custom Nginx config file into the container
COPY nginx.conf /etc/nginx/nginx.conf


# Expose the port that the app will run on
EXPOSE 80

# Start the Nginx server
CMD ["nginx", "-g", "daemon off;"]
