import { FilterModel } from './filter.model';
import { toLonLat } from 'ol/proj';
import { Params } from '@angular/router';
import { PhraseListModel } from './phrase-list.model';

/**
 * Search params for the filter endpoint
 */
export class SearchParams {
    uwis: string;
    states: string;
    basin: string;
    county: string;
    field: string;
    minDepth: number;
    maxDepth: number;
    minVintage: number;
    maxVintage: number;
    phrases: string;
    minPhraseCount: number;
    maxPhraseCount: number;
    minPhraseScore: number;
    maxPhraseScore: number;
    lat1: number;
    lon1: number;
    lat2: number;
    lon2: number;

    fromFiler(filter: FilterModel): SearchParams {
        this.uwis = filter.api;
        if (filter.states && filter.states.length > 0) {
            this.states = filter.states.map(each => each.value).join(',');
        }

        if (filter.basin) {
            this.basin = filter.basin.value;
        }

        if (filter.county) {
            this.county = filter.county.value;
        }

        if (filter.field) {
            this.field = filter.field.value;
        }
        this.minDepth = filter.depth.min;
        this.maxDepth = filter.depth.max;
        this.minVintage = filter.vintage.min;
        this.maxVintage = filter.vintage.max;
        if (filter.phrase && filter.phrase.phrases.length > 0) {
            const phrases = filter.phrase.phrases.map(item => item.value).join(',');
            this.phrases = phrases;
        }

        this.minPhraseCount = filter.phrase.count.min;
        this.maxPhraseCount = filter.phrase.count.max;
        this.minPhraseScore = filter.phrase.score.min;
        this.maxPhraseScore = filter.phrase.score.max;
        if (filter && filter.map && filter.map.extent) {
            const extent: number[] = filter.map.extent;
            const lonLat1 = toLonLat([extent[0], extent[1]]);
            const lonLat2 = toLonLat([extent[2], extent[3]]);
            this.lat1 = lonLat1[1];
            this.lon1 = lonLat1[0];
            this.lat2 = lonLat2[1];
            this.lon2 = lonLat2[0];
        }
        return this;
    }

    fromQueryParams(params: Params): SearchParams {
        for (const param in params) {
            if (params.hasOwnProperty(param)) {
                this[param] = params[param];
            }
        }
        return this;
    }

    updateFilter(filter: FilterModel): FilterModel {
        filter.api = this.uwis;
        if (this.states && this.states !== '') {
            if (!filter.states) {
                filter.states = [];
            }
            const stateArray = this.states.split(',');
            if (stateArray.length > 0) {
                filter.states.length = 0;
            }
            stateArray.forEach(item => {
                if (item !== 'all-states') {
                    filter.states.push({ label: item.trim(), value: item.trim() });
                }
            });
        }

        if (this.basin && this.basin !== '') {
            filter.basin = { value: this.basin, label: this.basin };
        }

        if (this.county && this.county !== '') {
            filter.county = { value: this.county, label: this.county };
        }


        if (this.field && this.field !== '') {
            filter.field = { value: this.field, label: this.field };
        }

        if (this.phrases && this.phrases !== '') {
            const phraseArray = this.phrases.split(',');
            const existingPhrases = filter.phrase.phrases;
            if (existingPhrases.length > 0) {
                existingPhrases.length = 0;
            }
            phraseArray.forEach(item => {
                existingPhrases.push({ label: item.trim(), value: item.trim(), alias: item.trim(), range: { min: 0, max: 0 } });
            });
        }

        filter.depth.min = this.minDepth;
        filter.depth.max = this.maxDepth;
        filter.vintage.min = this.minVintage;
        filter.vintage.max = this.maxVintage;
        filter.phrase.count.min = this.minPhraseCount;
        filter.phrase.count.max = this.maxPhraseCount;
        filter.phrase.score.min = this.minPhraseScore;
        filter.phrase.score.max = this.maxPhraseScore;
        if (filter.map && filter.map.extent) {
            filter.map.extent.length = 0;
            filter.map.extent.push(this.lon1);
            filter.map.extent.push(this.lat1);
            filter.map.extent.push(this.lon2);
            filter.map.extent.push(this.lat2);
        }

        return filter;
    }
}
