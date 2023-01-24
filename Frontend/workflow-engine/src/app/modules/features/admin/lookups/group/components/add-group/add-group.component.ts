import { Component, OnInit } from '@angular/core';
import { BaseEditComponent } from 'base/components/base-edit-component';
import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { FormGroup, Validators } from '@angular/forms';
import { GroupService } from '../../services/group.service';
import { GroupMemberService } from '../../services/group-member.service';
import { GroupMember } from '../../models/groupMember';
import { Result } from 'shared/models/result';
import { HttpStatus } from 'shared/enum/httpStatus';
import { LoadOptions } from 'shared/components/data-table/models/LoadOpts';
import { Subject } from 'rxjs';
import { EmployeeService } from 'shared/services/employee/employee.service';
import { debounceTime, distinctUntilChanged, take } from 'rxjs/operators';

@Component({
    selector: 'app-add-group',
    templateUrl: './add-group.component.html',
    styleUrls: ['./add-group.component.scss'],
})
export class AddGroupComponent extends BaseEditComponent implements OnInit {

    groupMembers: GroupMember[] = [];
    employees: any[] = [];
    employee: any = {};
    scrollSize = 10;
    scrollPageNumber = 1;
    getPaginationOptions: LoadOptions = {
        pageSize: this.scrollSize,
        pageNumber: this.scrollPageNumber,
        searchCriteria: '',
        filter: {
            searchCriteria: '',
        },
    };
    employeeInputSearch$ = new Subject();
    get Service(): GroupService { return Shell.Injector.get(GroupService); }
    get GroupMemberService(): GroupMemberService { return Shell.Injector.get(GroupMemberService); }
    get EmployeeService(): EmployeeService { return Shell.Injector.get(EmployeeService); }
    constructor(public override route: ActivatedRoute) {
        super(route);
    }

    override ngOnInit(): void {
        this.searchEmployeesOnType();
        this.initForm();
        super.ngOnInit();
        if (!this.isNew) {
            this.getGroupMembers();
        }
    }



    initForm(): void {
        this.form = this.Fb.group({
            id: [this.isNew ? '' : this.id],
            nameEn: ['', Validators.required],
            nameAr: [null, Validators.required],
            code: [null]
        });
    }


    initGroupMember(): FormGroup {
        return this.Fb.group({
            civilNumber: ['', Validators.required]
        });
    }

    getGroupMembers(): void {
        this.GroupMemberService.getByGroupId(this.id).subscribe((res: Result<GroupMember[]>) => {
            this.groupMembers = res.data;
        })
    }


    deleteMember(member: GroupMember): void {
        this.GroupMemberService.deleteMember(member.groupId, member.civilNumber).subscribe((res: Result<any>) => {
            console.log(res);
            if (res.status == HttpStatus.Accepted) {
                this.Alert.showSuccess(this.Localize.translate.instant('Validation.' + res.message));
                this.getGroupMembers();
                return;
            }
            this.Alert.showError(this.Localize.translate.instant('Validation.' + res.message));
            return;


        });
    }
    /**
     * Search Employee
     */
    searchEmployeesOnType(): void {
        this.employeeInputSearch$
            .pipe(debounceTime(500), distinctUntilChanged())
            .subscribe((searchValue: any) => {
                if (searchValue) {
                    this.scrollPageNumber = 1;
                    this.getPaginationOptions.pageNumber = this.scrollPageNumber;
                    this.getPaginationOptions.filter.searchCriteria = searchValue;
                    this.employees = [];
                    this.getPagedEmployees();
                }
            });
    }
    /**
     * Get Paged Employees
     */
    getPagedEmployees(): void {
        this.EmployeeService.getPagedEmployees(this.getPaginationOptions)
            .pipe(take(1))
            .subscribe((resp: any) => {
                this.scrollPageNumber += 1;
                this.getPaginationOptions.pageNumber = this.scrollPageNumber;
                this.employees = this.employees.concat(resp.result.data);
            });
    }

    add(): void {
        if (this.employee) {
            var exist = this.groupMembers.some(x => x.civilNumber == this.employee.nationalId);
            if (exist) {
                this.Alert.showError(this.Localize.translate.instant('Validation.CivilIdAlreadyExist'));
                return;
            }

            const model: GroupMember = {
                groupId: this.id,
                nameEn: this.employee.fullNameEn,
                nameAr: this.employee.fullNameAr,
                civilNumber: this.employee.nationalId,
                employeeEmail: this.employee.email,
                id: null
            }
            this.GroupMemberService.add(model).subscribe((res: any) => {
                if (res.status == 201) {
                    this.Alert.showSuccess(this.Localize.translate.instant('Validation.AddSuccess'));
                    this.employee = {};
                    this.getGroupMembers();
                    return;
                }
                this.Alert.showError(this.Localize.translate.instant('Validation.' + res.message));
                return;
            })
        }
    }
}
