export class Silo {
  siloId: string;
  name: string;
  description: string;

  constructor(
    siloId: string,
    name: string,
    description: string
  ) {
    this.siloId = siloId;
    this.name = name;
    this.description = description;
  }
}
