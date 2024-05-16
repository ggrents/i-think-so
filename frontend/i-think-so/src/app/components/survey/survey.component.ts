import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RadioButtonModule } from 'primeng/radiobutton';

@Component({
  selector: 'app-survey',
  standalone: true,
  imports: [ButtonModule, RadioButtonModule, CommonModule, FormsModule],
  templateUrl: './survey.component.html',
  styleUrl: './survey.component.scss',
})
export class SurveyComponent {
  options: any[] = [
    { id: 1, name: 'Java' },
    { id: 2, name: 'C#' },
    { id: 3, name: 'Python' },
  ];
  choice: any;
}
