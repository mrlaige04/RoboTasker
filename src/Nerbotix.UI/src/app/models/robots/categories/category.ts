import {CategoryProperty} from './category-property';
import {CategoryBase} from './category-base';

export class Category extends CategoryBase {
  properties!: CategoryProperty[];
  isMaximization?: boolean;

  constructor(obj?: Partial<Category>) {
    super(obj);
    if (obj) {
      Object.assign(this, obj);
    }
  }
}
