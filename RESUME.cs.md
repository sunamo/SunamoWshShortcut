---
schema_version: 11
type: library
category_override: none
file_count: 12
file_extensions: cs:5, csproj:3, md:3, noext:1, slnx:1
file_extensions_updated: 2026-10-04
avg_lines_per_file: 20
total_lines: 245
metrics_lm: 2026-10-03 16:20:00
move_to_legacy_percent: 0
description_updated: 2026-10-03
links_updated: 2026-10-03
github_source_url: not run
origin_status: own
origin_checked: not run
article_source_url: not run
article_status: own
article_checked: not run
last_build_ok: yes
last_build_date: 2026-10-03
last_tests_run_date: 2026-10-03
covered_lines: not run
---

## Description

Knihovna pro Windows, která vytváří zástupce (.lnk) přes Windows Script Host s pozdní vazbou. Nepotřebuje COM interop assembly, takže nevzniká konflikt s Fody.Costura, kvůli kterému původní kód z repa `DelaPotizePriLoadInVsCode` nešlo načíst.

## Původ zdrojáků

Staženo z GitHubu: **ne** — vlastní kód, převzatý z uživatelova repa `DelaPotizePriLoadInVsCode` (Azure DevOps `radekjancik`).

- Ověřeno: původní třída `WshHelper.CreateLnk` přepsána bez COMReference na pozdní vazbu.

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **0 %** — živý balíček ve wnp.

- Malá aktivní knihovna s testy.

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: žádné
