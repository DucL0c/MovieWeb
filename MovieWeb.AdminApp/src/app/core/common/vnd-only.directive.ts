import {
  Directive,
  OnInit,
  forwardRef,
  ElementRef,
  HostListener,
  Renderer2,
} from '@angular/core';
import { DecimalPipe } from '@angular/common';
/* ------------------------------------------------------------------------ *
    Requirements:
      1. format digits automatically (for good UX)
      2. disallow non-number characters (programatically format user input)
      3. dollar format, cents are not required (decimal is not supported eg. xxx.01)
      4. US locale only

    Unrequired:
      1. negation (negative numbers are not supported)
      2. cents are not required (xxx.01 is not supported)
      3. localization / globalization (is not supported)

    Notes:
    I began with a digit-only-directive.
    But instead of testing characters lets simply replace disallowed characters
    and format the input at the same time with regex.
    https://codeburst.io/digit-only-directive-in-angular-3db8a94d80c3

    If globalization is a requirement don't reinvent it, customize your pipe(s)
    https://stackoverflow.com/a/43323477/1440240

    TODO:
    Extend to use cents. First 2 digits are always .xx
* ------------------------------------------------------------------------ */

@Directive({
  selector: '[vnd-only]',
})
export class VndOnlyDirective implements OnInit {
  currencyChars = new RegExp('[.,]', 'g'); // we're going to remove commas and dots

  constructor(
    public el: ElementRef,
    public renderer: Renderer2,
    private decimalPipe: DecimalPipe
  ) {}

  ngOnInit() {
    this.format(this.el.nativeElement.value); // format any initial values
  }

  @HostListener('input', ['$event.target.value']) onInput(e: string) {
    this.format(e);
  }

  format(val: string) {
    // 1. test for non-number characters and replace/remove them
    const numberFormat = parseInt(String(val).replace(this.currencyChars, ''));
    // console.log(numberFormat); // raw number

    // 2. format the number (add commas)
    const vnd = this.decimalPipe.transform(numberFormat, '1.0', 'en-US');

    // 3. replace the input value with formatted numbers
    this.renderer.setProperty(this.el.nativeElement, 'value', vnd);
  }
}
