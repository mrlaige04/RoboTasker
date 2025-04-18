import {TenantEntity} from '../../common/base-tenant-entity';

export class CapabilityBase extends TenantEntity {
  name!: string;
  groupName!: string;
  description?: string;
  capabilitiesCount!: number;

  constructor(obj?: Partial<CapabilityBase>) {
    super(obj);
    if (obj) {
      Object.assign(this, obj);
    }
  }
}
