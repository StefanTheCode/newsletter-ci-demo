#!/usr/bin/env bash
# Module 09: fail a pull request that ADDS new diagnostic suppressions.
# Existing suppressions are not touched - only lines added by this PR are checked.
#
# Usage: scripts/check-suppressions.sh <base-ref>     e.g. origin/main
# Escape hatch: set SUPPRESSION_APPROVED=true (the workflow sets it when the PR has the
# label "suppression-approved"), so a justified suppression is a visible, reviewed decision.
set -euo pipefail

base="${1:-origin/main}"
pattern='#pragma warning disable|SuppressMessage|<NoWarn>|null!'

added=$(git diff --unified=0 "$base"...HEAD -- '*.cs' '*.csproj' '*.props' '*.targets' \
  | grep -E '^\+[^+]' | grep -E "$pattern" || true)

if [[ -z "$added" ]]; then
  echo "No new suppressions."
  exit 0
fi

echo "This pull request adds diagnostic suppressions:"
echo "$added"

if [[ "${SUPPRESSION_APPROVED:-false}" == "true" ]]; then
  echo "::warning title=Suppression approved::Label 'suppression-approved' is set - allowed."
  exit 0
fi

echo "::error title=New diagnostic suppression::Fix the code instead, or explain the suppression in the PR and add the label 'suppression-approved'."
exit 1
