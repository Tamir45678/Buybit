export interface PlaceOrder{
    UserId:number;
    ProductId:number;
    DeliveryDate:Date;
}

export interface OrderDetails {
    id:number
    userId:number;
    price:number;
    productId:number;
    deliveryDate:Date;
}