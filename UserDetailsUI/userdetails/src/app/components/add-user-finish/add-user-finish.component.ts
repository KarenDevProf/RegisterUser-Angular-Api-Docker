import { Component, OnInit } from '@angular/core';
import { Userdetails } from "src/app/models/Userdetails";
import { SelectOption } from "src/app/models/SelectOption";
import { UserdetailsService } from 'src/app/services/userdetails.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user-finish',
  templateUrl: './add-user-finish.component.html',
  styleUrls: ['./add-user-finish.component.css']
})
export class AddUserFinishComponent implements OnInit {
  countryDropdownOptions: SelectOption[] = [];
  provinceDropdownOptions: SelectOption[] = [];
  submitted = false;
  hasError = false;
  errorMessage = '';
  successMessage = false;
  saveButtonEnabled = false;
  locationForm!: FormGroup;

  constructor(
    private userdetailsService: UserdetailsService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadCountryDropdownOptions();
    this.watchFormChanges();
  }

  private buildForm(): void {
    this.locationForm = this.formBuilder.group({
      country: ['', Validators.required],
      province: [{ value: '', disabled: true }, Validators.required]
    });
  }

  private loadCountryDropdownOptions(): void {
    this.userdetailsService.getCountryDropdownOptions().subscribe(options => {
      this.countryDropdownOptions = options;
    });
  }

  private watchFormChanges(): void {
    this.locationForm.valueChanges.subscribe(() => {
      this.saveButtonEnabled =
        this.locationForm.valid &&
        this.locationForm.get('country')?.value !== null &&
        this.locationForm.get('province')?.value !== null;
    });

    this.locationForm.get('country')?.valueChanges.subscribe(value => {
      if (value) {
        this.enableProvinceDropdown();
        this.loadProvinceDropdownOptions(value);
      } else {
        this.disableProvinceDropdown();
        this.provinceDropdownOptions = [];
      }
      this.resetProvinceValue();
    });
  }

  private enableProvinceDropdown(): void {
    this.locationForm.get('province')?.enable();
  }

  private disableProvinceDropdown(): void {
    this.locationForm.get('province')?.disable();
  }

  private loadProvinceDropdownOptions(countryId: string): void {
    this.userdetailsService.getProvinceDropdownOptions(countryId).subscribe(options => {
      this.provinceDropdownOptions = options;
    });
  }

  private resetProvinceValue(): void {
    this.locationForm.get('province')?.setValue('');
  }

  get f() {
    return this.locationForm.controls;
  }

  saveUser() {
    this.submitted = true;
    if (!this.saveButtonEnabled || this.locationForm.invalid) {
      return;
    }

    const userInfo = JSON.parse(sessionStorage.getItem('userInfo') || '{}');
    const formData: Userdetails = {
      email: userInfo.email ?? '',
      password: userInfo.password ?? '',
      confirmPassword: userInfo.confirmPassword ?? '',
      countryId: this.locationForm.get('country')?.value,
      provinceId: this.locationForm.get('province')?.value
    };

    this.userdetailsService.createUser(formData).subscribe({
      next: res => {
        console.log(res);
        if (res.hasError) {
          this.errorMessage = res.message ?? 'An error occurred';
          this.hasError = true;
          return;
        }
        console.log(res);
      },
      complete: () => {
        if (this.hasError) {
          return;
        }

        this.successMessage = true;
        sessionStorage.removeItem('userInfo');
        setTimeout(() => {
          this.router.navigate(['/register']);
        }, 2000);
      },
      error: (e) =>{
        this.errorMessage = e.message ?? "An error occurred";
      } 
    });
  }

  goToStep1(){
    this.router.navigate(['/register'])
  }

}
       
