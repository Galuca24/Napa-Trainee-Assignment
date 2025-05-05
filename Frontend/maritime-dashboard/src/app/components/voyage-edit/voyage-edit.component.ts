import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidatorFn, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { Port } from '../../../models/port.model';
import { Ship } from '../../../models/ship.model';
import { Voyage } from '../../../models/voyage.model';

@Component({
  selector: 'app-voyage-edit',
  standalone: true,
  templateUrl: './voyage-edit.component.html',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatSelectModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule
  ]
})
export class VoyageEditComponent implements OnInit {
  form!: FormGroup;
  ports: Port[] = [];
  ships: Ship[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<VoyageEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { voyage: Voyage; ports: Port[]; ships: Ship[] }
  ) {}

  ngOnInit(): void {
    this.ports = this.data.ports || [];
    this.ships = this.data.ships || [];

    const voyage = this.data.voyage || {
      departurePortId: null,
      arrivalPortId: null,
      start: null,
      end: null,
      shipId: null
    };

    this.form = this.fb.group(
      {
        departurePortId: [voyage.departurePortId, Validators.required],
        arrivalPortId: [voyage.arrivalPortId, Validators.required],
        start: [voyage.start ? new Date(voyage.start) : null, Validators.required],
        end: [voyage.end ? new Date(voyage.end) : null, Validators.required],
        shipId: [voyage.shipId, Validators.required]
      },
      { validators: this.differentPortsValidator }
    );

    this.form.get('start')?.valueChanges.subscribe(value => {
      this.form.get('end')?.setValidators([Validators.required, this.minDateValidator(value)]);
      this.form.get('end')?.updateValueAndValidity();
    });
  }

  differentPortsValidator: ValidatorFn = (group: AbstractControl) => {
    const departure = group.get('departurePortId')?.value;
    const arrival = group.get('arrivalPortId')?.value;
    return departure && arrival && departure === arrival ? { samePorts: true } : null;
  };

  minDateValidator(minDate: Date): ValidatorFn {
    return (control: AbstractControl) => {
      const selected = new Date(control.value);
      return selected >= minDate ? null : { minDate: true };
    };
  }

  onStartChange(): void {
    const startValue = this.form.get('start')?.value;
    if (startValue) {
      this.form.get('end')?.setValidators([Validators.required, this.minDateValidator(startValue)]);
      this.form.get('end')?.updateValueAndValidity();
    }
  }

  save(): void {
    const startValue = this.form.get('start')?.value;
    if (startValue) {
      this.form.get('end')?.setValidators([Validators.required, this.minDateValidator(startValue)]);
      this.form.get('end')?.updateValueAndValidity();
    }

    if (this.form.valid) {
      const result = {
        id: this.data.voyage.id,
        ...this.form.value,
        voyageDate: this.form.value.start
      };
      this.dialogRef.close(result);
    }
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}
