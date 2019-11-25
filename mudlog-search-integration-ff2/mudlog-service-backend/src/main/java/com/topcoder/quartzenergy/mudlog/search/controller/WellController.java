package com.topcoder.quartzenergy.mudlog.search.controller;

import com.topcoder.quartzenergy.mudlog.search.exception.MudlogException;
import com.topcoder.quartzenergy.mudlog.search.dto.PaginatedWells;
import com.topcoder.quartzenergy.mudlog.search.dto.WellInfo;
import com.topcoder.quartzenergy.mudlog.search.service.ExcelExportService;
import com.topcoder.quartzenergy.mudlog.search.service.WellService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * Controller class for handling filtering wells
 */
@Slf4j
@RequestMapping("wells")
@RestController
public class WellController {

    /**
     * the well service instance
     */
    @Autowired
    private WellService wellService;

    /**
     * the exporter service instance
     */
    @Autowired
    private ExcelExportService exporter;

    /**
     * get all wells bounded by the extent of given lat-long
     *
     * @param minLat  latitude of top left corner of a rectangle
     * @param maxLat  latitude of bottom right corner of a rectangle
     * @param minLong longitude of top left corner of a rectangle
     * @param maxLong longitude of top bottom right of a rectangle
     * @return list of found Wells
     */
    @GetMapping
    public List<WellInfo> getAllWells(@RequestParam(name = "lat1") double minLat,
                                      @RequestParam(name = "lat2") double maxLat,
                                      @RequestParam(name = "lon1") double minLong,
                                      @RequestParam(name = "lon2") double maxLong) {
        return wellService.getAllWells(minLat, maxLat, minLong, maxLong);
    }

    /**
     * Search wells by given set of parameters
     *
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @param offset         list of rows to skip
     * @param limit          list of rows to return
     * @return list of found Wells
     */
    @GetMapping("/search")
    public PaginatedWells searchWells(
            @RequestParam(name = "uwis", required = false) List<String> uwis,
            @RequestParam(name = "states", required = false) List<String> states,
            @RequestParam(name = "basin", required = false) String basin,
            @RequestParam(name = "county", required = false) String county,
            @RequestParam(name = "field", required = false) String field,
            @RequestParam(name = "minDepth") int minDepth,
            @RequestParam(name = "maxDepth") int maxDepth,
            @RequestParam(name = "minVintage") int minVintage,
            @RequestParam(name = "maxVintage") int maxVintage,
            @RequestParam(name = "phrases", required = false) List<String> phrases,
            @RequestParam(name = "minPhraseCount") int minPhraseCount,
            @RequestParam(name = "maxPhraseCount") int maxPhraseCount,
            @RequestParam(name = "minPhraseScore") int minPhraseScore,
            @RequestParam(name = "maxPhraseScore") int maxPhraseScore,
            @RequestParam(name = "lat1", required = false) Double minLat,
            @RequestParam(name = "lat2", required = false) Double maxLat,
            @RequestParam(name = "lon1", required = false) Double minLong,
            @RequestParam(name = "lon2", required = false) Double maxLong,
            @RequestParam(name = "offset") int offset,
            @RequestParam(name = "limit") int limit) {

        return wellService.searchWells(uwis, states, basin, county, field, minDepth,
                maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong, offset, limit);
    }

    /**
     * Search and export wells found by applying given filters
     *
     * @param response       the http response
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @return response entity holding generated excel report in a blob
     */
    @GetMapping("/export")
    public ResponseEntity<?> exportExcel(
            HttpServletResponse response,
            @RequestParam(name = "uwis", required = false) List<String> uwis,
            @RequestParam(name = "states", required = false) List<String> states,
            @RequestParam(name = "basin", required = false) String basin,
            @RequestParam(name = "county", required = false) String county,
            @RequestParam(name = "field", required = false) String field,
            @RequestParam(name = "minDepth") int minDepth,
            @RequestParam(name = "maxDepth") int maxDepth,
            @RequestParam(name = "minVintage") int minVintage,
            @RequestParam(name = "maxVintage") int maxVintage,
            @RequestParam(name = "phrases", required = false) List<String> phrases,
            @RequestParam(name = "minPhraseCount") int minPhraseCount,
            @RequestParam(name = "maxPhraseCount") int maxPhraseCount,
            @RequestParam(name = "minPhraseScore") int minPhraseScore,
            @RequestParam(name = "maxPhraseScore") int maxPhraseScore,
            @RequestParam(name = "lat1", required = false) Double minLat,
            @RequestParam(name = "lat2", required = false) Double maxLat,
            @RequestParam(name = "lon1", required = false) Double minLong,
            @RequestParam(name = "lon2", required = false) Double maxLong
    ) {
        try {
            byte[] contentReturn = exporter.export(response, uwis, states, basin, county, field, minDepth,
                    maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                    minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong);

            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.parseMediaType("application/vnd.ms-excel"));
//            return new ResponseEntity<byte[]>(contentReturn, headers, HttpStatus.OK);
            return ResponseEntity.ok()
                    .headers(headers) // add headers if any
                    .contentLength(contentReturn.length)
                    .contentType(MediaType.parseMediaType("application/vnd.ms-excel"))
                    .body(contentReturn);
        } catch (MudlogException e) {
            log.error("Export error: ", e.getMessage());
            return new ResponseEntity<byte[]>(new byte[]{}, HttpStatus.INTERNAL_SERVER_ERROR);
        }

    }
}
