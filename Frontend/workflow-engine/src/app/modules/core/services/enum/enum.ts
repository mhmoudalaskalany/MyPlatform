export enum Action {
  Close = 'CLOSE',
  TransferToManager = 'TRANSFER-TO-MANAGER',
  TransferToLegalTeam = 'TRANSFER-TO-LEGALTEAM',
  TransferToCoordination = 'TRANSFER-TO-COORDINATION',
  TransferToAppealCourt = 'TRANSFER-TO-APPEAL-COURT',
  TransferToSupremeCourt = 'TRANSFER-TO-SUPREME-COURT',
  Adjourn = 'ADJOURN',
  PermanentClose = 'PERMANENT-CLOSE',
  Reopen = 'REOPEN'
}

export enum Status {
  NonActive = 1,
  Active = 2,
  DisAssembled = 3
}


export enum RoleCodes {
  LegalManager = 'LEGAL-AFFAIRS-MANAGER',
  LegalCoordinator = 'LEGAL-AFFAIRS-COORDINATOR',
  LegalTeam = 'LEGAL-AFFAIRS-LEGAL-TEAM',

}
