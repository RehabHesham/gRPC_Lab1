// package: 
// file: src/app/protos/Order.proto

var src_app_protos_Order_pb = require("../../../src/app/protos/Order_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var APIOrderService = (function () {
  function APIOrderService() {}
  APIOrderService.serviceName = "APIOrderService";
  return APIOrderService;
}());

APIOrderService.MakeOrder = {
  methodName: "MakeOrder",
  service: APIOrderService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_Order_pb.OrderDetail,
  responseType: src_app_protos_Order_pb.OrderResponse
};

exports.APIOrderService = APIOrderService;

function APIOrderServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

APIOrderServiceClient.prototype.makeOrder = function makeOrder(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(APIOrderService.MakeOrder, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.APIOrderServiceClient = APIOrderServiceClient;

