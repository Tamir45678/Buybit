import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PlaceOrder } from 'src/app/models/order.model';
import { orderDate } from 'src/app/models/orderDate.model';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.css']
})
export class AddProductModalComponent implements OnInit {

  shippingDate:any
  filterOldDays = (d:Date|null):boolean=>{
    return d >= new Date();
  }
  constructor(public dialogRef:MatDialogRef<AddProductModalComponent>,@Inject(MAT_DIALOG_DATA) public data:orderDate) { }

  ngOnInit(): void {
  }

  Cancel(): void {
    this.dialogRef.close();
  }

}
