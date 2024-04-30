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
    { id: 1, name: 'Первый вариантик вот такойй' },
    { id: 2, name: 'Второй вариантик вот такойй' },
    { id: 3, name: 'Третий вариантик вот такойй' },
  ];
  choice: any;
}
