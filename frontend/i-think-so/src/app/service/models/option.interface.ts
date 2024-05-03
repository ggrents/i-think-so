import { Vote } from './vote.interface';

export interface Option {
  title: string;
  choices?: Vote[];
}
