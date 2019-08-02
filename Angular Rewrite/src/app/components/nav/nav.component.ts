import { Component, OnInit, ViewChild } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GenericApiService, GenericModalComponent } from 'angular-generics';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'navigation',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavigationComponent implements OnInit {
@ViewChild('uploadModal', { static: false}) modal: GenericModalComponent;

  isHandset$: Observable<boolean> =
    this.breakpointObserver
      .observe(Breakpoints.Handset)
      .pipe(map(result => result.matches));

  showUploadModal: boolean = false;

  hasFiles: boolean = false;
  formData: FormData;

  constructor(private breakpointObserver: BreakpointObserver, public api: GenericApiService) {

  }

  ngOnInit() {

  }

  fileChanged(e: any) {
    let files = e.target.files;
    if (files && files.length > 0) {
      this.formData = new FormData();

      for (let x = 0; x < files.length; x++) {
        this.formData.append('file', files[x], files[x].name);
      }

      this.hasFiles = true;
    }
  }

  uploadFiles() {
    this.api.post(`${environment.apiRoot}/upload/inventory`, this.formData)
      .then(() => {
        this.hasFiles = false;
        this.modal.close();
      });

    // this.http.post('https://localhost:5001/api/upload', formData, { reportProgress: true, observe: 'events' })
    //   .subscribe(event => {
    //     if (event.type === HttpEventType.UploadProgress)
    //       this.progress = Math.round(100 * event.loaded / event.total);
    //     else if (event.type === HttpEventType.Response) {
    //       this.message = 'Upload success.';
    //       this.onUploadFinished.emit(event.body);
    //     }
    //   });
  }
}
