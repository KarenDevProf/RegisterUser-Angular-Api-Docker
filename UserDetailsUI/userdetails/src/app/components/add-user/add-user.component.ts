import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl,FormGroup , FormBuilder,  Validators } from '@angular/forms';
import { Router } from '@angular/router';
import  Validation  from 'src/app/utils/validation';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router
     ) { }

  ngOnInit(): void {
    this.buildForm(); 
    this.initializeFormFromSessionStorage()
  }
  
  private buildForm(): void {
    this.form = this.formBuilder.group(
      {
        email: ['', 
        [
          Validators.required, Validators.email,
          Validators.maxLength(50)
        ]
      ],
        password: [
          '',
          [
            Validators.compose([
              Validators.required,
              Validators.minLength(8),
              Validators.maxLength(40),
              Validation.patternValidator(new RegExp("(?=.*[0-9])"), {
                requiresDigit: true
              }),
              Validation.patternValidator(new RegExp("(?=.*[A-Z])"), {
                requiresUppercase: true
              }),
              Validation.patternValidator(new RegExp("(?=.*[a-z])"), {
                requiresLowercase: true
              }),
              Validation.patternValidator(new RegExp("(?=.*[$@^!%*?&])"), {
                requiresSpecialChars: true
              })
            ])
          ],
        ],
        confirmPassword: ['', [Validators.required,Validators.minLength(8)]],
        acceptTerms: [false, [Validators.requiredTrue]],
      },
      {
        validators: [Validation.match('password', 'confirmPassword')]
      }
    );
  }

  form = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    acceptTerms: new FormControl(false),
  });

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  get passwordValid() {
    return this.form.controls["password"].errors === null;
  }

  get requiredValid() {
    return !this.form.controls["password"].hasError("required");
  }

  get minLengthValid() {
    return !this.form.controls["password"].hasError("minlength");
  }

  get requiresDigitValid() {
    return !this.form.controls["password"].hasError("requiresDigit");
  }

  get requiresUppercaseValid() {
    return !this.form.controls["password"].hasError("requiresUppercase");
  }

  get requiresLowercaseValid() {
    return !this.form.controls["password"].hasError("requiresLowercase");
  }

  get requiresSpecialCharsValid() {
    return !this.form.controls["password"].hasError("requiresSpecialChars");
  }

  goToStep2(){
    console.log(this.form.value)
    this.submitted = true; 
    if (this.form.invalid) { 
      return;
    }
  
    sessionStorage.setItem('userInfo', JSON.stringify(this.form.value))
   
    this.router.navigate(['/register-step2'])
  }

  initializeFormFromSessionStorage() {
    const formData = JSON.parse(sessionStorage.getItem('userInfo') || '{}');
    if (formData) {
      this.form.patchValue(formData);
    }
  }
  
}

