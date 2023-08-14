# ApplicationA

ApplicationA is a MAUI application integrated with EmbedIO to serve as a REST API server along with web socket support. This application can communicate with another application (ApplicationB) through a REST API to enable streaming data via a web socket. You can use this to stream various types of data, such as positions or sensor data from ApplicationA to ApplicationB.

## **Features**

  REST API Server: Built with EmbedIO, it exposes an endpoint for controlling the streaming process.
  
  WebSocket Integration: ApplicationA can start and stop streaming data through web sockets.
  
  Cross-Platform Support: Targeted for multiple platforms including Android, with specific URL configurations.

## **How to Use**
### **Starting the Server**
Launch ApplicationA.

Click the "Start Server" button to initialize the server.

* For Android, the server will run at http://10.0.2.2:9696/.

* For other platforms, the server will run at http://localhost:9696/.

### **Starting Streaming**

With the server running, click the "Start Streaming" button.

The data streaming will begin, and a timestamp will be displayed on the screen.

### **REST API Endpoints**

* Start Streaming: GET http://<url>/api/start

* This endpoint allows you to initiate the streaming process.

### **WebSocket Module**

The DataWebSocketModule class handles WebSocket connections. It supports functionalities such as:

* Starting and stopping streaming.

* Sending and receiving messages from clients.

* Managing client connections and disconnections.

### **StreamingController**

This class exposes the API endpoint for starting the streaming process. When called, it triggers the DataWebSocketModule.StartStreaming method.

### **Contributing**
Feel free to fork the repository, make changes, and submit pull requests.

