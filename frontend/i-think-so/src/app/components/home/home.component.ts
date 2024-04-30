import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { SurveyComponent } from '../survey/survey.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, SurveyComponent, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
