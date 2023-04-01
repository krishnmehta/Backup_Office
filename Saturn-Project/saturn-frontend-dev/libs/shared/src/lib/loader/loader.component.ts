import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoaderService } from '../_shared-services/loader.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'saturn-frontend-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss'],
})
export class LoaderComponent implements OnInit, OnDestroy{

  isLoading: boolean;
  subscription: Subscription | undefined;

  constructor(private loaderService: LoaderService){
    this.isLoading = false;
  }

  ngOnInit(): void {
      this.subscription = this.loaderService.getLoader().subscribe(data => {
        this.isLoading = data;
      });
  }

  ngOnDestroy(): void {
      this.subscription?.unsubscribe();
  }
}
