import { Option } from './option.interface';

export interface Survey {
  id: string;
  title: string;
  userName: string;
  imageUrl?: string;
  options: Option[];
  createdAt: Date;
}
