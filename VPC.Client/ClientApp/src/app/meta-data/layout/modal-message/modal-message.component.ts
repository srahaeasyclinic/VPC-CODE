import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-message',
  templateUrl: './modal-message.component.html',
  styleUrls: ['./modal-message.component.css']
})
export class ModalMessageComponent implements OnInit {

  public title : string;
  public message : string;

  constructor(
    public messageActiveModal: NgbActiveModal,
  ) { }

  ngOnInit() {
  }

}
