import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Feed } from './feed/feed';
import { FeedRoutingModule } from './feed-routing-module';

@NgModule({
  declarations: [Feed],
  imports: [
    CommonModule,
    FormsModule,
    FeedRoutingModule
  ],
})
export class FeedModule {}