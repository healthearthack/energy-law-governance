/**
 * Energy Law & Global Governance Engine
 * Strict TypeScript interfaces for statutory frameworks, tax incentives, and battery passports.
 */

export interface IRAStatutoryBenefits {
  statute: string;
  section_30d_clean_vehicle_qualified: boolean;
  domestic_extraction_processing_pct: number;
  section_45x_production_credit_eligible: boolean;
  annual_production_cost_usd: number;
  estimated_annual_45x_tax_credit_usd: number;
  foreign_entity_of_concern_feoc_status: string;
}

export interface EUCRMACompliance {
  regulation: string;
  battery_passport_ready: boolean;
  iso_14044_lca_verified: boolean;
  lifecycle_emissions_rating: string;
  third_country_dependency_risk_index: number;
  human_rights_due_diligence: string;
}

export interface EPAUICRepermitting {
  statutory_authority: string;
  existing_wellbore_classification: string;
  target_classification: string;
  state_primacy_agency: string;
  permitting_cycle_savings_months: number;
  capex_savings_vs_greenfield_usd: number;
  area_of_review_aor_radius_miles: number;
  mechanical_integrity_test_mit_passed: boolean;
}

export interface StatutoryComplianceBundle {
  audit_title: string;
  auditor: string;
  timestamp_utc: string;
  corporate_diligence_summary: {
    overall_bankability_status: "INVESTMENT_GRADE_BANKABLE" | "HIGH_REGULATORY_RISK";
    statutory_tax_credit_annual_usd: number;
    permitting_pathway: string;
    global_export_eligibility: string;
  };
  united_states_ira: IRAStatutoryBenefits;
  european_union_crma: EUCRMACompliance;
  epa_underground_injection_control: EPAUICRepermitting;
}
