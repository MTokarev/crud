import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() modalTitle: string = '';
  @Input() modalMessage: string = '';
  @Input() displayStyle: string = 'none';
  @Input() hasError: boolean = false;
  @Output() modalClosed = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  closePopup() {
    this.displayStyle = 'none';
    this.modalClosed.emit();
  }

}
