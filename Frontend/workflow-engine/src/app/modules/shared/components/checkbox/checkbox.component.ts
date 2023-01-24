import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss']
})
export class CheckboxComponent implements OnInit {

  @Input() label = '';
  @Input() color = 'bg-primary';
  @Input() checked = false;
  @Input() formGroup: FormGroup;
  @Input() controlName = '';

  @Output() isChecked: EventEmitter<boolean> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
    this.checked = this.formGroup.get(this.controlName).value;
  }

  whenChecked(event) {
    this.formGroup.get(this.controlName).setValue(event.checked);
    this.checked = event.checked;
    this.isChecked.emit(event.checked);
  }
}
