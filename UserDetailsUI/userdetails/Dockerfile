# Stage 1: Build Angular app
FROM node:14-alpine as build
WORKDIR /app
COPY UserDetailsUI/userdetails/package*.json ./
RUN npm install
COPY UserDetailsUI/userdetails/ .
RUN npm run build

# Stage 2: Build Nginx image and copy Angular app
FROM nginx:alpine
COPY --from=build /app/dist/userdetails /usr/share/nginx/html
COPY UserDetailsUI/userdetails/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
