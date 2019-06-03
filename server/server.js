var app = require('express')();
var http = require('http').createServer(app);
var io = require('socket.io')(http);

app.get('/', function(req,res){
    res.sendFile(__dirname+'/index.html');
});

io.on('connection',function(socket){
    console.log('user connected')
    socket.broadcast.emit('hi');

    socket.on('chat message', function(msg){
        io.emit('message: '+ msg);
        console.log('message: ' + msg);
    });
    socket.on('disconnect',function(){
        console.log("user disconnected");
    });
});

http.listen(3000, function(){
    console.log("listining on: 3000")
});