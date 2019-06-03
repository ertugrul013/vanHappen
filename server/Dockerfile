FROM node:8

RUN mkdir -p /home/eddie-veteran/Documents/school/vanHappen/server/app

WORKDIR /home/eddie-veteran/Documents/school/vanHappen/server/app

COPY package*.json ./

RUN npm install 

COPY . .

EXPOSE 8080

CMD [ "npm", "start" ]