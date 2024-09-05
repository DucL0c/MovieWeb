import { NgModule } from '@angular/core';
import { VndOnlyDirective } from './vnd-only.directive';

@NgModule({
  exports: [VndOnlyDirective],
  declarations: [VndOnlyDirective],
})
export class VndModuleModule {}
