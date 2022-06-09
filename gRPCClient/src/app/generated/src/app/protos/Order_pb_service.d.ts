// package: 
// file: src/app/protos/Order.proto

import * as src_app_protos_Order_pb from "../../../src/app/protos/Order_pb";
import {grpc} from "@improbable-eng/grpc-web";

type APIOrderServiceMakeOrder = {
  readonly methodName: string;
  readonly service: typeof APIOrderService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_Order_pb.OrderDetail;
  readonly responseType: typeof src_app_protos_Order_pb.OrderResponse;
};

export class APIOrderService {
  static readonly serviceName: string;
  static readonly MakeOrder: APIOrderServiceMakeOrder;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class APIOrderServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  makeOrder(
    requestMessage: src_app_protos_Order_pb.OrderDetail,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_Order_pb.OrderResponse|null) => void
  ): UnaryResponse;
  makeOrder(
    requestMessage: src_app_protos_Order_pb.OrderDetail,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_Order_pb.OrderResponse|null) => void
  ): UnaryResponse;
}

