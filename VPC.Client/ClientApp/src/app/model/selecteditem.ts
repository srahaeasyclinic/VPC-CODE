import { ActiveValue } from './activevalue';

export class SelectedItem {
  name: string;
  sequence: number;
  hidden: boolean;
  dataType: string;
  refId: string;
  defaultValue: string;
  properties: string;
  values: ActiveValue[];
  clickable: boolean;
  defaultView: string;
  typeOf: string;
}
