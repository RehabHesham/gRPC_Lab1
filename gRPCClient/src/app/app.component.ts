import { Component } from '@angular/core';
import { grpc } from '@improbable-eng/grpc-web';
import { OrderDetail, Product } from './generated/src/app/protos/Order_pb';
import { APIOrderService } from './generated/src/app/protos/Order_pb_service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'gRPCClient';

  sendMessage(customerId:number, productId:number, productQuantity:number){
      
      let product = new Product();
      product.setProductid(productId);
      product.setProductquantity(productQuantity);
      
      let productList: Product[] = [];
      productList.push(product);
      
      let orderDetail  = new OrderDetail();
      orderDetail.setCustomerid(customerId);
      orderDetail.setProductsList(productList);

      grpc.unary(APIOrderService.MakeOrder, {
        request: orderDetail,
        host: "https://localhost:7290",
        onEnd: result => {
          const {status, message} = result;
          if (status == grpc.Code.OK && message) {
              console.log(JSON.stringify(message.toObject()));
          }
        }
      })
    }
}
