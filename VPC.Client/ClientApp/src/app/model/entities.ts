import { Configuration } from './configuration';
import { FieldModel } from './fieldmodel';
//import { ITreeNode} from '../model/treeNode';
import { Relation } from './relation';
import { Operation } from './operation';
import { Tasks } from './tasks';
import { RowLevelOperations } from './rowleveloperations';
import { Rule } from './rule';

export class Entities {
  displayName: string;
  name: string;
  pluralName: string;
  supportWorkflow: boolean;
  entityType: string;
  configurations: Configuration;
  subtypes: string[];
  fields: FieldModel[];
  relations: Relation[];
  operations: Operation[];
  tasks: Tasks[];
  rules: Rule[];
  relatedEntities: Entities[];
  rowLevelOperations: RowLevelOperations[];
  detailEntities:any;
  activityEntity:Entities;
}
