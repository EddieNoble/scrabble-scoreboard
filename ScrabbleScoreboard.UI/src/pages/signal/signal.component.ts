import { Component, signal, effect } from '@angular/core';

@Component({
  selector: 'app-signal',
  templateUrl: './signal.component.html'
})
export class SignalComponent {

  counter = signal(0);

    constructor()
    {
        effect(() =>
        {
            console.log(
                'I detected a change:',
                this.counter());
        });
    }

  testSignal(): void {
    this.counter.update(x => x + 1);
  }


}