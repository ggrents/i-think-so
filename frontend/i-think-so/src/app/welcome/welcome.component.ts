import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.scss',
})
export class WelcomeComponent {
  ngOnInit() {
    console.log(localStorage.getItem('1'));
  }

  setItem() {
    localStorage.setItem('1', '2');
  }
}
