// package: 
// file: src/app/protos/Order.proto

import * as jspb from "google-protobuf";

export class OrderDetail extends jspb.Message {
  getCustomerid(): number;
  setCustomerid(value: number): void;

  clearProductsList(): void;
  getProductsList(): Array<Product>;
  setProductsList(value: Array<Product>): void;
  addProducts(value?: Product, index?: number): Product;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): OrderDetail.AsObject;
  static toObject(includeInstance: boolean, msg: OrderDetail): OrderDetail.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: OrderDetail, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): OrderDetail;
  static deserializeBinaryFromReader(message: OrderDetail, reader: jspb.BinaryReader): OrderDetail;
}

export namespace OrderDetail {
  export type AsObject = {
    customerid: number,
    productsList: Array<Product.AsObject>,
  }
}

export class Product extends jspb.Message {
  getProductid(): number;
  setProductid(value: number): void;

  getProductquantity(): number;
  setProductquantity(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Product.AsObject;
  static toObject(includeInstance: boolean, msg: Product): Product.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Product, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Product;
  static deserializeBinaryFromReader(message: Product, reader: jspb.BinaryReader): Product;
}

export namespace Product {
  export type AsObject = {
    productid: number,
    productquantity: number,
  }
}

export class OrderResponse extends jspb.Message {
  getResponsemessage(): boolean;
  setResponsemessage(value: boolean): void;

  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): OrderResponse.AsObject;
  static toObject(includeInstance: boolean, msg: OrderResponse): OrderResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: OrderResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): OrderResponse;
  static deserializeBinaryFromReader(message: OrderResponse, reader: jspb.BinaryReader): OrderResponse;
}

export namespace OrderResponse {
  export type AsObject = {
    responsemessage: boolean,
    message: string,
  }
}

