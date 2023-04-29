FROM node:18.16.0-alpine as builder

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN rm -rf ./.env.local
RUN npm run build

FROM nginx:alpine

COPY ./nginx.conf /etc/nginx/nginx.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=builder /app/out /usr/share/nginx/html

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]