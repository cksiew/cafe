import { Gender } from './Gender';

export interface IEmployee {
  id?: string,
  name: string,
  emailAddress: string,
  phoneNumber: number,
  gender: Gender | number,
  daysWork?: number,
  cafeName?: string,
  cafeId: string
}