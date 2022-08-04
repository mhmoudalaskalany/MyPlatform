export abstract class HttpServiceBaseService {
  protected abstract get baseUrl(): string;
  protected abstract get serverUrl(): string;
  constructor() { }
}
