# Dealer Data Consolidation – Decisions

## 1. Matching strategy

Source records are first mapped into a common `DealerRecordDTO` and normalised before matching.

Normalisation includes:

- Upper-casing text values.
- Collapsing repeated whitespace.
- Removing common company-name suffixes such as `LIMITED`, `LTD` and `(UK)`.
- Removing formatting from postcodes.
- Normalising phone numbers to digits.
- Normalising website values by removing protocol, `www.` and trailing `/`.

The primary matching implementation is `DealerMatcher`.

The matching strategy reference required by the assessment is:

`JF-DDB-07`

### Matching scores

| Evidence | Score |
|---|---:|
| Exact company number | 100 |
| Exact public/name match | 40 |
| Legal/trading name relationship | 35 |
| Postcode match | 30 |
| Website match | 15 |
| Phone match | 15 |
| Town match | 10 |

Name evidence is mutually exclusive so the same underlying identity is not awarded multiple name scores.

Scores are capped at 100.

The minimum automatic same-dealer threshold is:

`DedupeConfidenceFloor = 80`

Records scoring below this threshold are not automatically consolidated.

## 2. Same-dealer decision

An exact company-number match is treated as definitive.

Otherwise, a record is considered the same dealer when its score meets or exceeds `DedupeConfidenceFloor`.

The threshold is deliberately conservative to reduce false-positive consolidation. Matching on a name alone, or on location alone, is not considered sufficient.

Where records do not meet the threshold, they remain separate dealer candidates rather than being forcibly consolidated.

## 3. Conflict precedence

Where multiple sources provide information for the same consolidated dealer, source-specific information is retained where possible.

Companies House is treated as the preferred source for corporate/legal information, including:

- Legal/company name
- Company number
- Company status
- Company incorporation date
- Registered office information

SAF is treated as the preferred source for dealer/trading information, including:

- Trading name
- SAF status
- SAF expiry
- Dealer contact information where supplied

For fields where the preferred source does not provide a value, another matching source may supply the value.

Source lineage is retained through `LineageTag` so the originating source records can be identified.

## 4. Lineage

Each consolidated `Dealer` exposes a `LineageTag` containing the originating source keys that contributed to the dealer.

The format will identify both the source and source key, for example:

`CompaniesHouse:12345678;SAF:ABC123`

This allows the consolidated record to be traced back to its source records.

## 5. Assumptions

- Records supplied by the same source are assumed to represent that source's view of a dealer.
- Normalisation is performed before matching so that superficial formatting differences do not prevent otherwise valid matches.
- Company numbers are considered stronger identifiers than names or addresses.
- Postcodes are useful supporting evidence but are not sufficient on their own to establish identity.
- A legal name may legitimately differ from a dealer's trading name.
- Missing information is not treated as conflicting information.
- The supplied datasets are treated as the authoritative input for the assessment.

## 6. Rerun behaviour

The import process must be safe to rerun without creating duplicate consolidated dealers.

Source keys and matching information are therefore used when determining whether an incoming record has already contributed to an existing dealer.

Database migrations are applied before import processing.

## 7. Omitted / next steps

- Source coverage: The current implementation focuses on the Companies House and SAF sources. The remaining supplied sources (FCA, ICO, MarketCheck, VAT lookups and crawled dealer data) were not implemented within the available time. With more time, I would add source-specific importers and mappers for these formats and feed them through the same normalisation, matching and consolidation pipeline.

The following areas are intentionally kept simple unless the supplied data demonstrates a need for additional complexity:

- Fuzzy name matching.
- External validation of company, VAT, FCA or ICO information.
- Advanced address similarity.
- Manual review workflows for borderline matches.

These could be introduced in a production implementation, but the assessment prioritises an explainable and deterministic consolidation strategy.