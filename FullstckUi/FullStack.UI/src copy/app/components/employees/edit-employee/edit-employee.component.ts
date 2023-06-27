import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.models';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  employeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: ''
  };

  constructor(private route: ActivatedRoute, private employeesService: EmployeesService, private router: Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if(id) {
          //call api
          this.employeesService.getEmployes(id).subscribe({
            next: (response) => {
              this.employeDetails = response;
            }
          });
        }
      }
    })
  }

  updateEmployee() {
    this.employeesService.updateEmployes(this.employeDetails.id, this.employeDetails).subscribe({
      next: (response) => {
        this.router.navigate(['employees']);
      }
    });
  }

}
