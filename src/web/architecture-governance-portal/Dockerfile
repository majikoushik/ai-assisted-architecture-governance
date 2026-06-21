FROM node:22-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build:prod

FROM nginx:alpine AS runtime
COPY --from=build /app/dist/architecture-governance-portal/browser /usr/share/nginx/html
COPY nginx.conf.template /etc/nginx/templates/default.conf.template

ENV API_BASE_URL=http://api:8080

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
