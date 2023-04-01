import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'lowercaseHyphen'
})
export class LowercaseHyphenPipe implements PipeTransform {

  transform(value: string): string {
    const string = value.replace(/\s+/g,'-');
    return string.toLowerCase();
  }

}
