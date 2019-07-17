import { Target } from './target';

export class Rule {
  tenantId:string;
  id:string;
  entityId: string;
  entityName: string;
  ruleName: string;
  ruleType: string;  
  sourceList: Target[];
  targetList: Target[];
  updatedBy: string;  
}
export class RuleType {
  active:boolean;
  flagged:boolean;
  internalId:string;
  isDeletetd:boolean;
  text:string;
  selectedRuleType:{}
}

